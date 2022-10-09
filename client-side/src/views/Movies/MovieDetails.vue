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
          <div style="margin-top: 60px; margin-bottom: 60px">
            <v-sheet
              class="d-flex justify-content-center mx-auto"
              elevation="10"
              max-height="1000"
            >
              <v-slide-group
                v-model="model"
                class="d-flex justify-content-center pa-4 ma-2"
                active-class="success"
                show-arrows
              >
                <v-slide-item
                  v-for="review in reviews"
                  :key="review.id"
                  v-slot="{ toggle }"
                  style="margin-top: 20px; margin-bottom: 20px"
                  class="d-flex justify-content-center"
                >
                  <div class="ml-2 mr-2">
                    <v-card @click="toggle">
                      <v-row align="center" justify="center">
                        <review-card
                          style="margin: 60px"
                          :hideDetails="true"
                          :movie="movie"
                        />
                      </v-row>
                    </v-card>
                  </div>
                </v-slide-item>
              </v-slide-group>
            </v-sheet>
          </div>
          <add-review />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { required, numberInt, minValueRule } from "@/helpers/validations";
import { setInteractionMode } from "vee-validate";
import MovieSchedules from "./MovieSchedules.vue";
import ReviewCard from "../../components/Reviews/ReviewCard.vue";
import AddReview from "../../components/Reviews/AddReview.vue";
import MovieActors from "./MovieActors.vue";

setInteractionMode("eager");

export default {
  components: {
    MovieSchedules,
    AddReview,
    ReviewCard,
    MovieActors,
  },
  data() {
    return {
      cinemaId: null,
      required,
      numberInt,
      minValueRule,
      todayDate: this.formatShortDateTime(new Date()),
      model: null,
    };
  },
  created() {
    this.movieId = this.$route.params.movieId;
    this.cinemaId = this.cinema.id;
    this.getMovie(this.movieId);
    this.getReviews(this.movieId);
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
    reviews() {
      return this.$store.state.reviews.review;
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
    getReviews(movieId) {
      const query = {
        movieId: movieId,
      };
      this.$store
        .dispatch("getReviews", query)
        .then(() => {})
        .catch((error) => {
          this.errorToast(
            error.response?.data?.errors[0] ||
              "Something went wrong while fetching reviews!"
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
