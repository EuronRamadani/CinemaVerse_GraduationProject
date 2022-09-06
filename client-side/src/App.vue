<template>
  <v-app>
    <public-layout v-if="!isAdminDashboard"> </public-layout>
    <dashboard-layout v-if="isAdminDashboard"> </dashboard-layout>
    <v-content class="ma-4" v-if="isAdminDashboard">
      <router-view></router-view>
    </v-content>
  </v-app>
</template>

<script>
import PublicLayout from "@/layouts/Public/PublicLayout.vue";
import DashboardLayout from "@/layouts/Dashboard/DashboardLayout.vue";
import { getAuth, onAuthStateChanged } from "firebase/auth";
import "firebase/auth";
export default {
  components: { PublicLayout, DashboardLayout },
  name: "App",

  data() {
    return {
      isAdmin: false,
    };
  },
  computed: {
    isAdminDashboard() {
      return this.$route.meta.layout === "dashboard";
    },
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
