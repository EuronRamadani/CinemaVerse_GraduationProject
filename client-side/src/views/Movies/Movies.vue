<template>
  <div>
    <div v-if="loading"><loading-page /></div>
    <div v-else>
      <h1 class="subheading grey--text">Movies</h1>
      <v-container class="container d-flex justify-content-center flex-wrap">
        <div v-for="movie in movies" :key="movie.id" class="ml-5 mr-5">
          <movie-card :movie="movie" />
        </div>
      </v-container>
    </div>
  </div>
</template>

<script>
import MovieCard from "@/components/cards/MovieCard.vue";

export default {
  components: { MovieCard },
  data() {
    return {
      cinemaId: null,
    };
  },
  watch: {
    cinema() {
      return this.getMovies();
    },
  },
  computed: {
    loading() {
      return this.$store.state.movies.loading;
    },
    cinema() {
      return this.$store.state.cinemas.cinema;
    },
    movies() {
      return this.$store.state.movies.movies;
    },
  },
  created() {
    this.cinemaId = this.$route.params.cinemaId;
    this.getMovies();
  },
  methods: {
    onDetailsClick(movieId) {
      this.$router.push({
        name: "Movie",
        params: { movieId: movieId },
      });
    },
    getMovies() {
      this.$store
        .dispatch(
          "getMovies",
          this.cinemaId == null ? this.cinema.id : this.cinemaId
        )
        .then(() => {
          if (this.movies.length > 0) {
            if (this.movies.photos.length > 0) {
              this.movie.photos.forEach((photo) => {
                require(photo.imgClientPath);
              });
            }
          }
        })
        .catch((error) => {
          this.errorToast(
            error.response?.data?.errors[0] ||
              "Something went wrong while fetching movies!"
          );
        });
    },
  },
};
</script>
