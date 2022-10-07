<template>
  <div>
    <v-card :loading="loading" class="mx-auto my-12" max-width="374">
      <v-img
        v-if="movie.photos.length > 0"
        min-width="250"
        min-height="250"
        :src="movie.photos[0].imgClientPath"
      ></v-img>
      <v-img
        v-else
        min-width="250"
        min-height="250"
        src="http://localhost:8080/assets/app_files/Movies/default-image.jpg"
      ></v-img>

      <v-card-title>{{ movie.title }}</v-card-title>
      <div
        v-if="!hideDetails"
        class="pa-2 d-flex justify-content-center align-center justify-content-between"
      >
        <v-rating :value="4.5" color="amber" dense half-increments></v-rating>
        <span class="grey--text ms-4">4.5 (413)</span>
      </div>
      <span v-if="!hideDetails" class="pa-1 d-flex align-center">
        <span class="ml-2 mr-2 align-center">Relase Year:</span>
        {{ movie.releaseYear }}</span
      >
      <div v-if="!hideDetails" class="ml-4 my-4 text-subtitle-1">
        <v-icon light>language</v-icon>
        • {{ `${movie.country}/${movie.language}` }}
      </div>
      <v-divider class="mx-4"></v-divider>
      <h6 class="ml-5">Genres</h6>
      <v-card-text>
        <v-chip-group active-class="deep-purple accent-4 white--text" column>
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
</template>

<script>
export default {
  data() {
    return {};
  },
  props: {
    hideDetails: {
      default: false,
      required: false,
    },
    movie: {
      type: Object,
    },
  },
  computed: {
    loading() {
      return this.$store.state.movies.loading;
    },
  },
  methods: {
    onDetailsClick(movieId) {
      this.$router.push({
        name: "Movie",
        params: { movieId: movieId },
      });
    },
  },
};
</script>
