<template>
  <div>
    <div v-if="loading"><loading-page /></div>
    <div v-else>
      <h1 class="subheading grey--text">Movies</h1>
      <v-container class="container d-flex justify-content-center flex-wrap">
        <div v-for="movie in movies" :key="movie.id" class="ml-5 mr-5">
          <v-card :loading="loading" class="mx-auto my-12" max-width="374">
            <v-img
              v-if="movie.photos.length > 0"
              height="250"
              :src="movie.photos[0].imgClientPath"
            ></v-img>
            <v-img
              v-else
              height="250"
              src="http://localhost:8080/assets/app_files/Movies/default-image.jpg"
            ></v-img>

            <v-card-title>{{ movie.title }}</v-card-title>
            <div
              class="pa-2 d-flex justify-content-center align-center justify-content-between"
            >
              <v-rating
                :value="4.5"
                color="amber"
                dense
                half-increments
              ></v-rating>
              <span class="grey--text ms-4">4.5 (413)</span>
            </div>
            <span class="pa-2 d-flex align-center">
              <span class="mr-2 align-center">Relase Year:</span>
              {{ movie.releaseYear }}</span
            >
            <div class="pa-2 my-4 text-subtitle-1">
              <v-icon light>language</v-icon>
              • {{ `${movie.country}/${movie.language}` }}
            </div>
            <v-divider class="mx-4"></v-divider>
            <v-card-title>Genres</v-card-title>
            <v-card-text>
              <v-chip-group
                active-class="deep-purple accent-4 white--text"
                column
              >
                <v-chip>{{ movie.genre }}</v-chip>
              </v-chip-group>
            </v-card-text>

            <v-card-actions class="d-flex justify-content-between">
              <v-btn
                outlined
                color="deep-purple lighten-2"
                text
                @click="onDetailsClick(movie.id)"
              >
                See Details
              </v-btn>
              <v-btn
                outlined
                color="primary lighten-2"
                text
                @click="onDetailsClick(movie.id)"
              >
                See Schedule
              </v-btn>
            </v-card-actions>
          </v-card>
        </div>
      </v-container>
    </div>
  </div>
</template>

<script>
export default {
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
