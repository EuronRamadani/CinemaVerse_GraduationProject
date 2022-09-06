import Vue from "vue";
import Vuex from "vuex";

import app from "./app/index";

//Users
import users from "./Users/users";

const initialState = {
  users: { ...users.state },
};

Vue.use(Vuex);

export default new Vuex.Store({
  modules: {
    app,
    users,
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
