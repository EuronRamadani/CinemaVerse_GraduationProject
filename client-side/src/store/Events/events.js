import api from "@/libs/api";

export default {
  state: {
    looading: false,
    events: [],
  },
  mutations: {
    SET_LOADING(state, value) {
      state.loading = value;
    },
    SET_EVENTS(state, payload) {
      state.events = payload;
    },
  },
  actions: {
    getEvents({ commit }, eventId) {
      commit("SET_LOADING", true);
      return new Promise((resolve, reject) => {
        api("events")
          .get(`cinemas/${eventId}/events`)
          .then((response) => {
            commit("SET_EVENTS", response.data.result);
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
