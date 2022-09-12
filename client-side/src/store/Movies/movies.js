import api from "@/libs/api";

export default {
  state: {
    looading: false,
    movies: [],
  },
  mutations: {
    SET_LOADING(state, value) {
      state.loading = value;
    },
    SET_MOVIES(state, payload) {
      state.movies = payload;
    },
  },
  actions: {
    getMovies({ commit }, cinemaId) {
      commit("SET_LOADING", true);
      return new Promise((resolve, reject) => {
        api("movies")
          .get(`cinemas/${cinemaId}/movies`)
          .then((response) => {
            commit("SET_MOVIES", response.data.result);
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
