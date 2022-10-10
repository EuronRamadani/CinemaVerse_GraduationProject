<template>
  <validation-observer ref="observer" v-slot="{ invalid }">
    <v-form ref="form" v-model="valid" lazy-validation>
      <validation-provider
        v-slot="{ errors }"
        name="Title"
        rules="required|min:5"
      >
        <v-text-field
          v-model="reviewTitle"
          name="reviewTitle"
          :counter="30"
          :error-messages="errors"
          :rules="nameRules"
          label="Review Title"
          required
        ></v-text-field>
      </validation-provider>
      <validation-provider
        v-slot="{ errors }"
        name="Description"
        rules="required"
      >
        <v-text-field
          v-model="reviewDescription"
          name="reviewDescription"
          :rules="descRules"
          :error-messages="errors"
          :counter="300"
          label="Review Description"
          required
        ></v-text-field>
      </validation-provider>
      <validation-provider name="reviewRating" rules="required">
        <label for="v-rating">Rating</label>
        <v-rating
          :value="0"
          name="reviewRating"
          style="margin-top: 10px; margin-bottom: 30px"
          color="amber"
          dense
          required
          v-model="reviewRating"
        ></v-rating>
      </validation-provider>

      <v-btn
        color="success"
        class="mr-4"
        :disabled="invalid"
        @click="createReview()"
      >
        Post Review
      </v-btn>

      <v-btn color="error" class="mr-4" @click="reset"> Reset Form </v-btn>
    </v-form>
  </validation-observer>
</template>

<script>
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
  data: () => ({
    valid: true,
    name: "",
    nameRules: [
      (v) => !!v || "Title is required",
      (v) => (v && v.length <= 30) || "Title must be less than 30 characters",
    ],
    descRules: [
      (v) => !!v || "Description is required",
      (v) =>
        (v && v.length <= 300) ||
        "Description must be less than 300 characters",
    ],
    reviewTitle: "",
    reviewDescription: "",
    userId: "",
    reviewRating: 0,
  }),

  created() {
    this.movieId = this.$route.params.movieId;
  },
  computed: {
    user() {
      return this.$store.state.users.user;
    },
  },
  methods: {
    validate() {
      this.$refs.form.validate();
    },
    reset() {
      this.$refs.form.reset();
    },

    createReview() {
      const review = {
        reviewTitle: this.reviewTitle,
        reviewTdescription: this.reviewTdescription,
        reviewRating: this.reviewRating,
        userId: this.user.id,
        userName: this.user.displayName,
      };
      this.$store
        .dispatch("createReview", { movieId: this.movieId, review: review })
        .then(() => {
          this.clear();
          this.$router.push({
            name: "Movies",
          });
        })
        .catch((error) => {
          console.log(error);
        });
    },

    clear() {
      (this.reviewTitle = ""),
        (this.reviewDescription = ""),
        (this.userId = ""),
        (this.reviewRating = 0),
        this.$refs.observer.reset();
    },
  },
};
</script>

<style></style>
