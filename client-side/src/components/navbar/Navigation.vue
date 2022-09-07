<template>
  <div>
    <v-app-bar app color="primary" dark>
      <div class="d-flex align-center mr-5">
        <v-list color="primary" class="d-flex">
          <!-- v-for="item in items" :key="item" -->
          <v-list-item :to="{ name: 'Home' }">
            <!-- "logo" -->
            <img :src="logo" alt="logo" class="logo" />
          </v-list-item>
        </v-list>
      </div>
      <div class="nav-links">
        <v-list color="primary" class="d-flex">
          <v-list-item v-for="item in items" :key="item" :to="{ name: item }">
            {{ item }}
          </v-list-item>
          <v-list-item v-if="user.isAdmin" :to="{ name: 'Admin' }">
            Admin
          </v-list-item>
        </v-list>
      </div>
      <v-spacer></v-spacer>
      <div class="nav-links">
        <v-list color="primary" class="d-flex">
          <v-list-item v-if="isLoggedIn">
            <v-avatar size="50">
              <img :src="user.photoURL" />
            </v-avatar>
          </v-list-item>
          <v-list-item v-if="isLoggedIn" class="nav-links">
            {{ user.displayName }}
          </v-list-item>
        </v-list>
      </div>
      <div class="my-2">
        <v-btn
          outlined
          text
          v-if="!isLoggedIn"
          :to="{ name: 'Login' }"
          class="mr-2"
        >
          <span>Login</span>
          <v-icon right>login</v-icon>
        </v-btn>
      </div>

      <div class="my-2">
        <v-btn outlined text v-if="!isLoggedIn" :to="{ name: 'Register' }">
          <span>Register</span>
          <v-icon right>person_add</v-icon>
        </v-btn>
      </div>

      <div class="my-2">
        <v-btn outlined text v-if="isLoggedIn" @click="handleSignOut">
          <span>Sign Out</span>
          <v-icon right>logout</v-icon>
        </v-btn>
      </div>
    </v-app-bar>
  </div>
</template>

<script>
import { signOut } from "firebase/auth";

export default {
  components: {},
  props: {
    isLoggedIn: {
      required: true,
      type: Boolean,
    },
    auth: {
      required: true,
    },
  },
  data() {
    return {
      items: ["Cinemas", "Movies", "Events"],
      logo: require("@/assets/main-logo_1.svg"),
    };
  },
  methods: {
    handleSignOut() {
      signOut(this.auth).then(() => {
        this.$store.commit("RESET_STATE");
        this.$router.push("/login");
        window.location.reload();
      });
    },
  },
  computed: {
    user() {
      return this.$store.state.users.user;
    },
  },
};
</script>

<style lang="scss" scoped>
.logo {
  width: 180px;
}
</style>
