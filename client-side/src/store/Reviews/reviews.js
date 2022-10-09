import api from "@/libs/api";

export default {
	state: {
		loading: false,
		fetchingReviews: false,
		removingReview: false,
		reviews: [],
		review: {},
	},
	getters: {
		reviewList(state) {
			return state.reviews;
		},
	},
	mutations: {
		SET_LOADING(state, value) {
			state.loading = value;
		},
		SET_REVIEWS(state, payload) {
			state.reviews = payload;
		},
		SET_REVIEW(state, payload) {
			state.review = payload;
		},
		REMOVE_REVIEW(state, id) {
			state.reviews = state.reviews.filter((h) => h.id !== id);
		},
		ADD_REVIEW(state, payload) {
			state.reviews.push(payload);
		},
	},
	actions: {
		getReviews({ commit }, query) {
			commit("SET_LOADING", true);
			return new Promise((resolve, reject) => {
				api("movies")
					.get(`movies/${query.movieId}/reviews`)
					.then((response) => {
						commit("SET_REVIEWS", response.data.result);
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
		removeReview({ commit }, query) {
			commit("SET_LOADING", true);
			return new Promise((resolve, reject) => {
				api("movies")
					.delete(`movies/${query.movieId}/reviews/${query.reviewId}`)
					.then((response) => {
						commit("REMOVE_review", query.reviewId);
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
		createReview({ commit }, query) {
			commit("SET_LOADING", true);
			return new Promise((resolve, reject) => {
				api("movies")
					.post(`movies/${query.movieId}/reviews`, query.review)
					.then((response) => {
						commit("ADD_REVIEW", query.review);
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
