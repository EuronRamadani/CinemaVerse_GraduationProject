<template>
  <div>
    <h2>Create Movie Times</h2>
    <hr />
    <div class="container mt-5">
      <validation-observer ref="observer" v-slot="{ invalid }">
        <form @submit.prevent="submit">
          <validation-provider
            v-slot="{ errors }"
            name="Hall Id"
            rules="required"
          >
            <v-text-field
              v-model="hallId"
              type="number"
              :error-messages="errors"
              label="Hall Id"
              outlined
              required
            ></v-text-field>
          </validation-provider>
          <validation-provider
            v-slot="{ errors }"
            name="Start Time"
            rules="required"
          >
            <v-text-field
              v-model="startTime"
              type="datetime-local"
              :min="formatShortDateTime(new Date())"
              :error-messages="errors"
              label="Start Time"
              outlined
              required
            ></v-text-field>
          </validation-provider>
          <validation-provider
            v-slot="{ errors }"
            name="End Time"
            rules="required"
          >
            <v-text-field
              v-model="endTime"
              type="datetime-local"
              :min="startTime"
              :error-messages="errors"
              label="End Time"
              outlined
              required
            ></v-text-field>
          </validation-provider>

          <v-btn
            color="success"
            type="submit"
            :disabled="invalid"
            class="mr-2"
            @click="createMovieTime()"
          >
            Submit
          </v-btn>
        </form>
      </validation-observer>
    </div>
  </div>
</template>

<script>
import { required, numberInt, minValueRule } from "@/helpers/validations";
import {
  ValidationObserver,
  ValidationProvider,
  setInteractionMode,
} from "vee-validate";

setInteractionMode("eager");

export default {
  components: {
    ValidationProvider,
    ValidationObserver,
  },
  data() {
    return {
      required,
      numberInt,
      minValueRule,
      cinemaId: null,
      hallId: 0,
      startTime: null,
      endTime: null,
    };
  },
  created() {
    this.cinemaId = this.$route.params.cinemaId;
    this.movieId = this.$route.params.movieId;
  },
  methods: {
    submit() {
      this.$refs.observer.validate();
    },
    createMovieTime() {
      const movieTime = {
        hallId: this.hallId,
        startTime: this.startTime,
        endTime: this.endTime,
      };
      this.$store
        .dispatch("createMovieTime", {
          cinemaId: this.cinemaId,
          movieId: this.movieId,
          movieTime: movieTime,
        })
        .then(() => {
          this.clear();
          this.$router.push({
            name: "MoviesDashboard",
          });
        })
        .catch((error) => {
          console.log(error);
          this.errorToast(
            error.response?.data ||
              "Something went wrong while creating movie times!"
          );
        });
    },
    clear() {
      this.hallId = 0;
      this.startTime = null;
      this.endTime = null;
      this.$refs.observer.reset();
    },
  },
};
</script>
