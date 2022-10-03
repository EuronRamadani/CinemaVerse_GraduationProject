import api from "@/libs/api";

export default {
  state: {
    loading: false,
    fetchingMovies: false,
    removingMovie: false,
    movies: [],
    movie: {},
  },
  mutations: {
    SET_LOADING(state, value) {
      state.loading = value;
    },
    SET_MOVIES(state, payload) {
      state.movies = payload;
    },
    SET_MOVIE(state, payload) {
      state.movie = payload;
    },
    REMOVE_MOVIE(state, id) {
      state.movies = state.movies.filter((h) => h.id !== id);
    },
    ADD_MOVIE(state, payload) {
      state.movies.push(payload);
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
    getMovie({ commit }, query) {
      commit("SET_LOADING", true);
      return new Promise((resolve, reject) => {
        api("movies")
          .get(`cinemas/${query.cinemaId}/movies/${query.movieId}`)
          .then((response) => {
            commit("SET_MOVIE", response.data.result);
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
    removeMovie({ commit }, query) {
      commit("SET_LOADING", true);
      return new Promise((resolve, reject) => {
        api("movies")
          .delete(`cinemas/${query.cinemaId}/movies/${query.movieId}`)
          .then((response) => {
            commit("REMOVE_MOVIE", query.movieId);
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
    editMovie({ commit }, query) {
      commit("SET_LOADING", true);
      return new Promise((resolve, reject) => {
        api("movies")
          .put(
            `cinemas/${query.cinemaId}/movies/${query.movie.id}`,
            query.movie
          )
          .then((response) => {
            commit("ADD_MOVIE", query.movie);
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
    createMovie({ commit }, query) {
      commit("SET_LOADING", true);
      return new Promise((resolve, reject) => {
        api("movies")
          .post(`cinemas/${query.cinemaId}/movies`, query.movie)
          .then((response) => {
            commit("ADD_MOVIE", query.movie);
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
    addMoviePhotos({ commit }, query) {
      commit("SET_LOADING", true);
      return new Promise((resolve, reject) => {
        const headers = {
          "Content-Type": "multipart/form-data",
        };
        api("movies")
          .post(
            `cinemas/${query.cinemaId}/movies/${query.movieId}/photos`,
            query.files,
            {
              headers: headers,
            }
          )
          .then((response) => {
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
    removeMoviePhoto({ commit }, query) {
      commit("SET_LOADING", true);
      return new Promise((resolve, reject) => {
        api("movies")
          .delete(
            `cinemas/${query.cinemaId}/movies/${query.movieId}/photos/${query.photoId}`
          )
          .then((response) => {
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
