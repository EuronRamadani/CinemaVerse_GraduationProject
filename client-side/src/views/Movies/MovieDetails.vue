<template>
  <div>
    <div v-if="loading"><loading-page /></div>
    <div v-else>
      <div class="container mt-5">
        <v-col class="d-flex align-center justify-content-center" cols="12">
          <div v-if="movie.photos.length > 0">
            <div class="d-flex justify-content-center flex-wrap w-100">
              <v-carousel>
                <v-carousel-item
                  v-for="(photo, i) in movie.photos"
                  :key="i"
                  :src="photo.imgClientPath"
                  min-width="1100"
                  max-width="1100"
                  reverse-transition="fade-transition"
                  transition="fade-transition"
                >
                </v-carousel-item>
              </v-carousel>
              <div>
                <v-row>
                  <v-col cols="12">
                    <span
                      >Title:
                      <h4>{{ movie.title }}</h4>
                    </span>
                  </v-col>
                  <v-col cols="12">
                    <span
                      >Description:
                      <h6>
                        {{ movie.description | truncate(600, "...") }}
                      </h6>
                    </span>
                  </v-col>

                  <v-col cols="12">
                    <span
                      >Release Year:
                      <h6>{{ movie.releaseYear }}</h6></span
                    >
                  </v-col>
                  <v-col cols="12">
                    <span
                      >Length:
                      <h6>{{ movie.length }}min</h6></span
                    >
                  </v-col>
                  <v-col cols="12">
                    <span
                      >Director:
                      <h6>
                        {{ movie.director }}
                      </h6></span
                    >
                  </v-col>
                  <v-col cols="12">
                    <div class="pa-2 my-4 text-subtitle-1">
                      <v-icon light>language</v-icon>
                      • {{ `${movie.country}/${movie.language}` }}
                    </div>
                  </v-col>
                </v-row>
              </div>
            </div>
          </div>
          <div v-else class="d-flex flex-column mb-5">
            <h2>There are no photos for this movie!</h2>
          </div>
        </v-col>
        <hr />
      </div>
      <div class="d-flex justify-content-center">
        <div class="container m0">
          <h1 class="d-flex justify-content-center mb-5">Cast</h1>
          <!-- Add this view to another view -->
          <!-- Add actors here -->
          <movie-actors />
        </div>
      </div>
      <hr />
      <div class="d-flex justify-content-center">
        <div class="container m0">
          <h1 class="d-flex justify-content-center mb-5">Today's Schedule</h1>
          <movie-schedules />
        </div>
      </div>
      <hr />
      <div
        style="margin-top: 50px; margin-bottom: 50px"
        class="d-flex justify-content-center"
      >
        <div class="container m0">
          <h1 class="d-flex justify-content-center mb-5">
            Reviews for this movie
          </h1>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { required, numberInt, minValueRule } from "@/helpers/validations";
import { setInteractionMode } from "vee-validate";
import MovieSchedules from "./MovieSchedules.vue";
import MovieActors from "./MovieActors.vue";

setInteractionMode("eager");

export default {
  components: { MovieSchedules, MovieActors },
  data() {
    return {
      cinemaId: null,
      required,
      numberInt,
      minValueRule,
      todayDate: this.formatShortDateTime(new Date()),
    };
  },
  created() {
    this.movieId = this.$route.params.movieId;
    this.cinemaId = this.cinema.id;
    this.getMovie(this.movieId);
  },
  filters: {
    truncate: function (text, length, suffix) {
      if (text.length > length) {
        return text.substring(0, length) + suffix;
      } else {
        return text;
      }
    },
  },
  computed: {
    loading() {
      return this.$store.state.movies.loading;
    },
    movie() {
      return this.$store.state.movies.movie;
    },
    cinema() {
      return this.$store.state.cinemas.cinema;
    },
  },
  methods: {
    handleCHange() {
      console.log(this.todayDate);
    },
    submit() {
      this.$refs.observer.validate();
    },
    getMovie(movieId) {
      const query = {
        cinemaId: this.cinemaId,
        movieId: movieId,
      };
      this.$store
        .dispatch("getMovie", query)
        .then(() => {
          this.getMovieTimes();
        })
        .catch((error) => {
          this.errorToast(
            error.response?.data?.errors[0] ||
              "Something went wrong while fetching movie details!"
          );
        });
    },
    getMovieTimes() {
      const query = {
        cinemaId: this.cinema.id,
        movieId: this.movie.id,
      };
      this.$store.dispatch("getMovieTimes", query).catch((error) => {
        this.errorToast(
          error.response?.data?.errors[0] ||
            "Something went wrong while fetching movie times!"
        );
      });
    },
  },
};
</script>
