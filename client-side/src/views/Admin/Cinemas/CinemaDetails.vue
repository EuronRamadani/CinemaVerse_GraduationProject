<template>
  <div>
    <h2>Cinema Details</h2>
    <hr />
    <div class="container mt-5">
      <!-- <v-col cols="12" md="4">
        <v-item>
          <v-carousel>
            <v-carousel-item
              v-for="(photo, i) in cinema.photos"
              :key="i"
              :src="require(photo.imgClientPath)"
              reverse-transition="fade-transition"
              transition="fade-transition"
            ></v-carousel-item>
          </v-carousel>
        </v-item>
      </v-col> -->

      <v-item-group active-class="primary">
        <v-container>
          <v-row>
            <v-col cols="12" md="4">
              <v-item>
                <span
                  >Name:
                  <h6>{{ cinema.name }}</h6></span
                >
              </v-item>
            </v-col>
            <v-col cols="12" md="4">
              <v-item>
                <span
                  >Number Of Venues:
                  <h6>{{ cinema.numberOfVenues }}</h6></span
                >
              </v-item>
            </v-col>

            <v-col cols="12" md="4">
              <v-item>
                <span
                  >City:
                  <h6>{{ cinema.city }}</h6></span
                >
              </v-item>
            </v-col>
            <v-col cols="12" md="4">
              <v-item>
                <span
                  >Address:
                  <h6>{{ cinema.address }}</h6></span
                >
              </v-item>
            </v-col>
            <v-col cols="12" md="4">
              <v-item>
                <span
                  >Description:
                  <h6>{{ cinema.description }}</h6></span
                >
              </v-item>
            </v-col>
          </v-row>
        </v-container>
      </v-item-group>
      <div></div>
    </div>
  </div>
</template>

<script>
import { required, numberInt, minValueRule } from "@/helpers/validations";
import { setInteractionMode } from "vee-validate";

setInteractionMode("eager");

export default {
  components: {},
  data() {
    return {
      cinemaId: null,
      required,
      numberInt,
      minValueRule,
    };
  },
  created() {
    this.cinemaId = this.$route.params.cinemaId;
    this.getCinema(this.cinemaId);
  },
  computed: {
    loading() {
      return this.$store.state.cinemas.loading;
    },
    cinema() {
      return this.$store.state.cinemas.cinema;
    },
  },
  methods: {
    submit() {
      this.$refs.observer.validate();
    },
    getCinema(cinemaId) {
      this.$store
        .dispatch("getCinema", cinemaId)
        .then(() => {
          this.cinema.photos.forEach((photo) => {
            require(photo.imgClientPath);
          });
        })
        .catch((error) => {
          this.errorToast(
            error.response?.data?.errors[0] ||
              "Something went wrong while fetching cinema!"
          );
        });
    },
    editCinema() {
      const cinema = {
        id: this.cinemaId,
        name: this.cinema.name,
        description: this.cinema.description,
        city: this.cinema.city,
        address: this.cinema.address,
        numberOfVenues: this.cinema.numberOfVenues,
      };
      this.$store
        .dispatch("editCinema", cinema)
        .then(() => {
          this.clear();
          this.$router.push({
            name: "CinemasDashboard",
          });
        })
        .catch((error) => {
          this.errorToast(
            error.response?.data?.errors[0] ||
              "Something went wrong while creating cinemas!"
          );
        });
    },
    clear() {
      this.name = "";
      this.description = "";
      this.city = "";
      this.address = "";
      this.numOfVenues = 0;
      this.$refs.observer.reset();
    },
  },
};
</script>
