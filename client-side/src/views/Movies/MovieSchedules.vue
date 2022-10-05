<template>
  <div>
    <div v-if="loading"><loading-page /></div>
    <div v-else>
      <div>
        <v-btn color="success" class="mr-2" @click="getMovieTimes()">
          Generate Schedules
        </v-btn>
      </div>
      <v-sheet class="mx-auto" elevation="8">
        <v-sheet
          class="d-flex justify-content-center mx-auto"
          elevation="10"
          max-height="1000"
        >
          <v-slide-group
            class="d-flex justify-content-center ma-2"
            active-class="success"
            show-arrows
          >
            <v-slide-item
              v-for="movieTime in movieTimes"
              :key="movieTime.id"
              class="mb-2 d-flex justify-content-center"
            >
              <div class="ml-2 mr-2">
                <v-card @click="onDetailsClick(movie.id)" class="ma-2">
                  <v-row align="center" justify="center">
                    <v-card class="mx-auto" max-width="300" outlined>
                      <div class="d-flex">
                        <v-icon>confirmation_number</v-icon>
                        <v-list-item three-line>
                          <v-list-item-content>
                            <div class="text-overline mb-4"></div>
                            <v-list-item-title class="text-h5 mb-1">
                              Hall:
                              {{
                                (movieTime.hall && movieTime.hall.name) || "-"
                              }}
                            </v-list-item-title>
                            <v-list-item-subtitle
                              >Start Time:
                              {{
                                formatDateTime(movieTime.startTime)
                              }}</v-list-item-subtitle
                            >
                            <v-list-item-subtitle
                              >End Time:
                              {{
                                formatDateTime(movieTime.endTime)
                              }}</v-list-item-subtitle
                            >
                          </v-list-item-content>
                        </v-list-item>
                      </div>
                    </v-card>
                  </v-row>
                </v-card>
              </div>
            </v-slide-item>
          </v-slide-group>
        </v-sheet>
      </v-sheet>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {};
  },
  created() {
    // this.getMovieTimes();
    console.log(this.movieTimes);
  },
  watch: {
    movie() {
      return this.getMovieTimes();
    },
  },
  computed: {
    loading() {
      return this.$store.state.movieTimes.loading;
    },
    cinema() {
      return this.$store.state.cinemas.cinema;
    },
    movie() {
      return this.$store.state.movies.movie;
    },
    movieTimes() {
      return this.$store.state.movieTimes.movieTimes;
    },
  },
  methods: {
    onDetailsClick(movieTimeId) {
      this.$router.push({
        name: "MovieTime-Details",
        params: {
          cinemaId: this.cinema.id,
          movieId: this.movie.id,
          movieTimeId: movieTimeId,
        },
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
