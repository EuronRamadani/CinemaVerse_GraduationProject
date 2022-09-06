<template>
  <v-app>
    <navigation />
    <v-content class="ma-4">
      <router-view />
    </v-content>
    <app-footer />
  </v-app>
</template>

<script>
import Navigation from "@/components/navbar/Navigation.vue";
import AppFooter from "@/components/footer/Footer.vue";

export default {
  components: { Navigation, AppFooter },
  name: "App",

  data() {
    return {};
  },
  computed: {},
};
</script>
