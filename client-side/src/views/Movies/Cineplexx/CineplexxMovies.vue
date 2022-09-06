<template>
  <div class="card">
    <v-card class="mx-auto" max-width="344">
      <v-img
        src="https://cdn.vuetifyjs.com/images/cards/sunshine.jpg"
        height="200px"
      ></v-img>

      <v-card-title> Movie1 </v-card-title>

      <v-card-subtitle> Genre: </v-card-subtitle>
      <v-card-actions>
        <v-btn color="orange lighten-2" text> View </v-btn>

        <v-spacer></v-spacer>
      </v-card-actions>
    </v-card>

    <v-card class="mx-auto" max-width="344">
      <v-img
        src="https://cdn.vuetifyjs.com/images/cards/sunshine.jpg"
        height="200px"
      ></v-img>

      <v-card-title> Movie2 </v-card-title>

      <v-card-subtitle> Genre: </v-card-subtitle>
      <v-card-actions>
        <v-btn color="orange lighten-2" text> View </v-btn>

        <v-spacer></v-spacer>
      </v-card-actions>
    </v-card>
  </div>
</template>

<script>
export default {};
</script>

<style scoped>
.card {
  flex-direction: row;
  justify-content: center;
}
</style>
