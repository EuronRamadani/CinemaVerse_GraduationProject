<template>
  <b-card
    no-body
    class="overflow-hidden mx-auto"
    style="max-width: 540px; margin: 50px"
  >
    <b-row no-gutters>
      <b-card-img
        :src="event.photos[0].imgClientPath"
        alt="Image"
        class="rounded-0"
      ></b-card-img>
      <b-col md="6" class="text-center mx-auto">
        <b-card-body :title="event.name">
          <b-card-text>
            {{ event.description }}
          </b-card-text>
          <b-card-text> <b> Price: </b>{{ event.price }}€ </b-card-text>
          <b-card-text>
            {{ event.date }}
          </b-card-text>
          <b-button @click="onDetailsClick(event.id)" variant="primary"
            >Details</b-button
          >
        </b-card-body>
      </b-col>
    </b-row>
  </b-card>
</template>

<script>
export default {
  components: {},
  props: {
    event: Object,
  },
  computed: {
    events() {
      return this.$store.state.events.events;
    },
    cinema() {
      return this.$store.state.cinemas.cinema;
    },
  },
  methods: {
    removeEvent(eventId) {
      this.$store.dispatch("removeEvent", eventId).then(() => {
        window.location.reload();
      });
    },
    onDetailsClick(eventId) {
      this.$router.push({
        name: "Event",
        params: { eventId: eventId },
      });
    },
  },
};
</script>

<style lang="scss" scoped>
</style>
