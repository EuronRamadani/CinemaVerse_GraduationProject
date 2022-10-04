<template>
  <v-sheet class="mx-auto" elevation="8" max-width="800">
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
          v-for="movie in 15"
          :key="movie.id"
          v-slot="{ toggle }"
          class="d-flex justify-content-center"
        >
          <div class="ml-2 mr-2">
            <v-card @click="toggle">
              <v-row align="center" justify="center">
                <!-- change later -->
                <h6>schedule</h6>
                <!-- <movie-card :hideDetails="true" :movie="movie" /> -->
              </v-row>
            </v-card>
          </div>
        </v-slide-item>
      </v-slide-group>
    </v-sheet>

    <v-expand-transition>
      <v-sheet v-if="model != null" height="200" tile>
        <v-row class="fill-height" align="center" justify="center">
          <h3 class="text-h6">Selected {{ model }}</h3>
        </v-row>
      </v-sheet>
    </v-expand-transition>
  </v-sheet>
</template>

<script>
export default {
  data: () => ({
    model: null,
  }),
};
</script>
