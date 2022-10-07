<template>
  <div>
    <h1 class="d-flex justify-content-center">Movies</h1>
    <div v-if="loading"><loading-page /></div>
    <div v-else>
      <div class="d-flex justify-content-center flex-wrap">
        <div v-for="movie in movies" :key="movie.id" class="ml-5 mr-5">
          <movie-card :movie="movie" />
        </div>
      </div>
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
        .dispatch("getMovies", this.cinema.id)
        .then(() => {})
        .catch((error) => {
          console.log(error);
          this.errorToast(
            error.response?.data?.errors[0] ||
              "Something went wrong while fetching movies!"
          );
        });
    },
  },
};
</script>
