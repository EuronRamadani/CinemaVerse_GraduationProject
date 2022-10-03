<template>
  <div class="container d-flex justify-content-center flex-wrap">
    <div v-for="cinema in cinemas" :key="cinema.id" class="ml-5 mr-5">
      <v-card :loading="loading" class="mx-auto my-12" max-width="374">
        <template slot="progress">
          <v-progress-linear
            color="deep-purple"
            height="10"
            indeterminate
          ></v-progress-linear>
        </template>

        <v-img height="250" :src="cinema.photos[0].imgClientPath"></v-img>

        <v-card-title>{{ cinema.name }}</v-card-title>

        <v-card-text>
          <v-row align="center" class="mx-0">
            <v-rating
              :value="4.5"
              color="amber"
              dense
              half-increments
              readonly
              size="14"
            ></v-rating>

            <div class="grey--text ms-4">4.5 (413)</div>
          </v-row>

          <div class="my-4 text-subtitle-1">
            <v-icon light>place</v-icon>
            • {{ `${cinema.city}, ${cinema.address}` }}
          </div>
        </v-card-text>

        <v-divider class="mx-4"></v-divider>

        <v-card-title>Tonight's availability</v-card-title>

        <v-card-text>
          <v-chip-group active-class="deep-purple accent-4 white--text" column>
            <v-chip>5:30PM</v-chip>

            <v-chip>7:30PM</v-chip>

            <v-chip>8:00PM</v-chip>

            <v-chip>9:00PM</v-chip>
          </v-chip-group>
        </v-card-text>

        <v-card-actions class="d-flex justify-content-between">
          <v-btn
            outlined
            color="deep-purple lighten-2"
            text
            @click="onDetailsClick(cinema.id)"
          >
            See Details
          </v-btn>
          <v-btn
            outlined
            color="primary lighten-2"
            text
            @click="onMoviesClick(cinema.id)"
          >
            See Movies
          </v-btn>
        </v-card-actions>
      </v-card>
    </div>
  </div>
</template>

<script>
export default {
  name: "Cinemas",
  data() {
    return {
      search: "",
    };
  },
  created() {
    this.getCinemas();
  },
  computed: {
    loading() {
      return this.$store.state.cinemas.loading;
    },
    cinemas() {
      return this.$store.state.cinemas.cinemas;
    },
    filter() {
      return this.cinemas.filter((temp) => {
        return temp.name.match(this.search);
      });
    },
  },
  methods: {
    onDetailsClick(cinemaId) {
      this.$router.push({
        name: "cinema-public-details",
        params: { cinemaId },
      });
    },
    onMoviesClick(cinemaId) {
      this.$router.push({
        name: "Movies",
        params: { cinemaId },
      });
    },
    getCinemas() {
      this.$store.dispatch("getCinemas").catch((error) => {
        this.errorToast(
          error.response?.data?.errors[0] ||
            "Something went wrong while fetching cinemas!"
        );
      });
    },
  },
};
</script>

<style scoped>
.card {
  flex-direction: row;
  justify-content: center;
}
input {
  border: 1px solid black;
  padding: 20px;
  margin-top: 2%;
  margin-bottom: 2%;
  border-radius: 15px;
}
</style>
