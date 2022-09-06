<template>
  <!-- App.vue -->

  <v-app>
    <navigation :isLoggedIn="isLoggedIn" :auth="auth" />
    <!-- <v-navigation-drawer app>
  </v-navigation-drawer> -->

    <!-- Sizes your content based upon application components -->
    <v-main class="container">
      <!-- Provides the application the proper gutter -->
      <v-container fluid>
        <!-- If using vue-router -->
        <router-view></router-view>
      </v-container>
    </v-main>

    <app-footer />
  </v-app>
</template>

<script>
import Navigation from "@/components/navbar/Navigation.vue";
import AppFooter from "@/components/footer/Footer.vue";
import { getAuth, onAuthStateChanged } from "firebase/auth";
import "firebase/auth";

export default {
  components: { Navigation, AppFooter },
  name: "App",

  data() {
    return {};
  },

  mounted() {
    this.auth = getAuth();
    onAuthStateChanged(this.auth, (user) => {
      if (user) {
        this.isLoggedIn = true;
      } else {
        this.isLoggedIn = false;
      }
    });
  },
};
</script>

<style lang="scss" scoped>

</style>
