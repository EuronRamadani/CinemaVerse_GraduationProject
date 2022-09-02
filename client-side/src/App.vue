<template>
  <body class="app">
    <navigation :isLoggedIn="isLoggedIn" :auth="auth" />
    <router-view class="p-3" />
  </body>
</template>

<script>
import Navigation from "./components/navbar/Navigation.vue";
import { getAuth, onAuthStateChanged } from "firebase/auth";
import "firebase/auth";

export default {
  name: "app",
  components: {
    Navigation,
  },
  data() {
    return {
      isLoggedIn: false,
      auth: null,
    };
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
