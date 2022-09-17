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
		getMovie({ commit }, movieId) {
			commit("SET_LOADING", true);
			return new Promise((resolve, reject) => {
				api("movies")
					.get(`cinemas/${2}/movies/${movieId}`)
					.then((response) => {
						commit("SET_MOVIE", response.data.result);
						resolve(response);
						console.log(response);
					})
					.catch((error) => {
						reject(error);
					})
					.finally(() => {
						commit("SET_LOADING", false);
					});
			});
		},
		removeMovie({ commit }, movieId) {
			commit("SET_LOADING", true);
			return new Promise((resolve, reject) => {
				api("movies")
					.delete(`cinemas/${2}/movies/${movieId}`)
					.then((response) => {
						commit("REMOVE_MOVIE", movieId);
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
		editMovie({ commit }, movie) {
			commit("SET_LOADING", true);
			return new Promise((resolve, reject) => {
				api("movies")
					.put(`cinemas/${3}/movies/${movie.id}`, movie)
					.then((response) => {
						commit("ADD_MOVIE", movie);
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
		createMovie({ commit }, movie) {
			commit("SET_LOADING", true);
			return new Promise((resolve, reject) => {
				api("movies")
					.post(`cinemas/${2}/movies`, movie)
					.then((response) => {
						commit("ADD_MOVIE", movie);
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
