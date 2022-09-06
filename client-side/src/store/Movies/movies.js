export default {
	state: {
		movies: [],
	},

	getters: {
		moviesList(state) {
			return state.movies;
		},
	},

	mutations: {
		SET_LIST(state, payload) {
			state.movies = payload;
		},
	},
	actions: {
		fetchMovies({ commit }, payload) {
			commit("SET_LIST", payload);
		},
	},
};
