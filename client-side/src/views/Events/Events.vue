<template>
  <div>
    <input type="text" v-model="search" placeholder="Search events" />
    <div class="row">
      <Card v-for="entry in eventList" :key="entry.id" :event="entry" />
    </div>
  </div>
</template>

<script>
import Card from "../../components/Card.vue";
import { mapGetters } from "vuex";

export default {
  name: "events",
  components: {
    Card,
  },
  created() {
    this.getEvents();
  },
  computed: {
    events() {
      return this.$store.state.events.events;
    },
    cinema() {
      return this.$store.state.cinemas.cinema;
    },
    ...mapGetters({
      eventList: "eventList",
    }),
  },
  watch: {
    cinema() {
      return this.getEvents();
    },
  },
  methods: {
    getEvents() {
      this.$store.dispatch("getEvents", this.cinema.id).catch((error) => {
        this.errorToast(
          error.response?.data?.errors[0] ||
            "Something went wrong while fetching cinemas!"
        );
      });
    },
  },
};
</script>

<style lang="scss" scoped>
.card {
  flex-direction: row;
  justify-content: center;
}
input {
  border: 1px solid black;
  padding: 20px;
  margin-top: 2%;
  margin-bottom: 2%;
  border-radius: 15px;
}
</style>
