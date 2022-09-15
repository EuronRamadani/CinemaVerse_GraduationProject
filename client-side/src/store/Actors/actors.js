import api from "@/libs/api";

export default {
  state: {
    loading: false,
    actors: [],
  },
  mutations: {
    SET_LOADING(state, value) {
      state.loading = value;
    },
    SET_ACTORS(state, payload) {
      state.actors = payload;
    },
  },
  actions: {
    getEvents({ commit }) {
      commit("SET_LOADING", true);
      return new Promise((resolve, reject) => {
        api("movies")
          .get(`actors`)
          .then((response) => {
            commit("SET_ACTORS", response.data.result);
            resolve(response);
          })
          .catch((error) => {
            reject(error);
          })
          .finally(() => {
            commit("SET_LOADING", false);
          });
      });
    },
  },
};
