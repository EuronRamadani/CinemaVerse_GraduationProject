<template>
  <div>
    <b-navbar toggleable="lg" type="dark" variant="info" class="p-2">
      <b-navbar-brand href="#">"Logo"</b-navbar-brand>

      <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

      <b-collapse id="nav-collapse" class="justify-content-between" is-nav>
        <b-navbar-nav>
          <b-nav-item class="nav-links">
            <router-link class="router-link" :to="{ name: 'Home' }">
              Home
            </router-link>
          </b-nav-item>
          <b-nav-item class="nav-links">
            <router-link class="router-link" :to="{ name: 'Admin' }">
              Admin
            </router-link>
          </b-nav-item>
          <b-nav-item v-if="!isLoggedIn" class="nav-links">
            <router-link class="router-link" :to="{ name: 'Login' }">
              Login
            </router-link>
          </b-nav-item>
          <b-nav-item v-if="!isLoggedIn" class="nav-links">
            <router-link class="router-link" :to="{ name: 'Register' }">
              Register
            </router-link>
          </b-nav-item>
        </b-navbar-nav>

        <!-- Right aligned nav items -->
        <b-navbar-nav class="ml-auto align-items-center">
          <b-nav-form class="">
            <div class="d-flex justify-content-center">
              <b-form-input
                size="sm"
                class="mr-sm-2"
                placeholder="Search"
              ></b-form-input>
              <b-button size="sm" class="my-2 my-sm-0" type="submit"
                >Search</b-button
              >
            </div>
          </b-nav-form>

          <!-- <b-nav-item-dropdown text="Lang" right>
            <b-dropdown-item href="#">EN</b-dropdown-item>
            <b-dropdown-item href="#">ES</b-dropdown-item>
            <b-dropdown-item href="#">RU</b-dropdown-item>
            <b-dropdown-item href="#">FA</b-dropdown-item>
          </b-nav-item-dropdown> -->

          <b-nav-item v-if="isLoggedIn" class="nav-links">
            <b-avatar variant="info" :src="userImg" />
          </b-nav-item>

          <b-nav-item v-if="isLoggedIn" class="nav-links">
            {{ firstName }}
          </b-nav-item>

          <b-nav-item
            v-if="isLoggedIn"
            class="nav-links"
            @click="handleSignOut"
          >
            Sign Out
          </b-nav-item>
          <!-- <b-nav-item-dropdown right>
            <template #button-content>
              <em>User</em>
            </template>
            <b-dropdown-item href="#">Profile</b-dropdown-item>
            <b-dropdown-item href="#">Sign Out</b-dropdown-item>
          </b-nav-item-dropdown> -->
        </b-navbar-nav>
      </b-collapse>
    </b-navbar>
  </div>
</template>

<script>
import { signOut } from "firebase/auth";
export default {
  methods: {
    handleSignOut() {
      signOut(this.auth).then(() => {
        this.$router.push("/");
      });
    },
  },
  computed: {
    currentUser() {
      return this.auth?.currentUser;
    },
    firstName() {
      return this.currentUser.displayName;
    },
    userImg() {
      return this.currentUser.photoURL;
    },
  },
  props: {
    isLoggedIn: {
      required: true,
      type: Boolean,
    },
    auth: {
      required: true,
    },
  },
};
</script>

<style lang="scss" scoped>
.nav-links {
  ::v-deep {
    a {
      color: white !important;
    }
  }
}
</style>
