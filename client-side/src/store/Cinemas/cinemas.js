import api from "@/libs/api";

export default {
  state: {
    loading: false,
    fetchingCinemas: false,
    removingCinema: false,
    cinemas: [],
  },
  mutations: {
    SET_LOADING(state, value) {
      state.loading = value;
    },
    SET_CINEMAS(state, payload) {
      state.cinemas = payload;
    },
  },
  actions: {
    getCinemas({ commit }) {
      commit("SET_LOADING", true);
      return new Promise((resolve, reject) => {
        api("movies")
          .get(`cinemas`)
          .then((response) => {
            commit("SET_CINEMAS", response.data.result);
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
