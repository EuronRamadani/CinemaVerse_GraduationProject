<template>
  <body class="container">
    <h2>
      {{ event.name }}
    </h2>
    <div>
      <div class="events">
        <p>
          {{ event.description }}
        </p>
        <h6><b>Created At: </b> {{ event.date ? event.date : "--" }}</h6>
        <h6><b>Price: </b> {{ event.price }}</h6>
        <div>
          <v-btn color="success" class="mr-2" @click="goToEvent()">
            <v-icon left dark> mdi-plus </v-icon>
            Go to event
          </v-btn>
        </div>
      </div>
    </div>
    <hr />
  </body>
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
    goToEvent() {
      this.$store
        .dispatch("goToEvent", {
          cinemaId: this.cinema.id,
          eventId: this.event.id,
          event:this.event
        })
        .then(() => {
          this.$router.push("/movies");
        });
    },
    removeEvent(eventId) {
      this.$store.dispatch("removeEvent", eventId).then(() => {
        window.location.reload();
      });
    },
  },
};
</script>

<style lang="scss" scoped>
.container {
  h2 {
    text-align: center;
    align-self: center;
  }
}

.events {
  display: flex;
  flex-direction: row;
  padding: 15px;
  margin-right: 5%;
  margin-left: 5%;
  width: 90%;
  justify-content: space-between;
  flex-wrap: wrap;

  @media (max-width: 700px) {
    .events {
      flex-direction: row;
    }

    img {
      display: none;
    }

    h5 {
      margin-left: 5%;
      margin-right: 3%;
      padding: 10px;
      text-align: center !important;
      width: 100% !important;
    }
  }

  img {
    width: 35%;
    height: 100%;
  }

  h5 {
    margin-left: 5%;
    margin-right: 3%;
    font-size: 28px;
    padding: 10px;
    text-align: center;
    width: 15%;
  }

  p {
    margin-top: 3%;
    font-size: 23px;
    margin-bottom: 3%;
  }
}
</style>
