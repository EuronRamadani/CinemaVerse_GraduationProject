import api from "@/libs/api";

export default {
  state: {
    loading: false,
    fetchingEvents: false,
    removingEvent: false,
    events: [],
    event: {},
  },
  getters: {
    eventList(state) {
      return state.events;
    },
  },
  mutations: {
    SET_LOADING(state, value) {
      state.loading = value;
    },
    SET_EVENTS(state, payload) {
      state.events = payload;
    },
    SET_EVENT(state, payload) {
      state.event = payload;
    },
    REMOVE_EVENT(state, id) {
      state.events = state.events.filter((h) => h.id !== id);
    },
    ADD_EVENT(state, payload) {
      state.events.push(payload);
    },
  },
  actions: {
    getEvents({ commit }, eventId) {
      commit("SET_LOADING", true);
      return new Promise((resolve, reject) => {
        api("movies")
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
    getEvent({ commit }, query) {
      commit("SET_LOADING", true);
      return new Promise((resolve, reject) => {
        api("movies")
          .get(`cinemas/${query.cinemaId}/events/${query.eventId}`)
          .then((response) => {
            commit("SET_EVENT", response.data.result);
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
    removeEvent({ commit }, query) {
      commit("SET_LOADING", true);
      return new Promise((resolve, reject) => {
        api("movies")
          .delete(`cinemas/${query.cinemaId}/events/${query.eventId}`)
          .then((response) => {
            commit("REMOVE_EVENT", query.eventId);
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
    editEvent({ commit }, query) {
      commit("SET_LOADING", true);
      return new Promise((resolve, reject) => {
        api("movies")
          .put(
            `cinemas/${query.cinemaId}/events/${query.event.id}`,
            query.event
          )
          .then((response) => {
            commit("ADD_EVENT", query.event);
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
    createEvent({ commit }, query) {
      commit("SET_LOADING", true);
      return new Promise((resolve, reject) => {
        api("movies")
          .post(`cinemas/${query.cinemaId}/events`, query.event)
          .then((response) => {
            commit("ADD_EVENT", query.event);
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
    goToEvent({ commit }, query) {
      commit("SET_LOADING", true);
      return new Promise((reject) => {
        api("movies")
          .put(
            `cinemas/${query.cinemaId}/events/IncreaseAttendees/${query.eventId}`,
             query.event
          )
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
