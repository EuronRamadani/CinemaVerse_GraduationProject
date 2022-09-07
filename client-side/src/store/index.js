import Vue from "vue";
import Vuex from "vuex";
import createPersistedState from "vuex-persistedstate";

import app from "./app";

//Users
import users from "./Users/users";

import movies from "./Movies/movies";

const initialState = {
  users: { ...users.state },
};

Vue.use(Vuex);

export default new Vuex.Store({
  plugins: [
    createPersistedState({
      storage: window.sessionStorage,
    }),
  ],
  modules: {
    app,
    users,
    movies,
  },
  state: {},
  mutations: {
    RESET_STATE(state) {
      sessionStorage.clear();
      Object.keys(state).forEach((key) => {
        Object.assign(state[key], initialState[key]);
      });
    },
  },
  strict: process.env.DEV,
  actions: {},
  getters: {},
});
