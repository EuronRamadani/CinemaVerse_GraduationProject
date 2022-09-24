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
        <h6><b>Created At: </b> {{ event.date ? event.date : '--' }}</h6>
        <h6><b>Price: </b> {{ event.price }}</h6>
        </div>
      </div>
      <hr />
  </body>
</template>

<script>
export default {
  props: {
    event: Object,
  },
  computed: {
    events() {
      return this.$store.state.events.events;
    },
  },
  methods: {
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

  @media (max-width: 700px) {
    .btns {
      width: 190% !important;
      .link {
        width: 150%;
      }
    }
  }

  .btns {
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    padding: 15px;
    width: 30%;

    .link {
      padding: 15px;
    }

    button {
      background-color: black;
      border: none;
      color: white;
      padding: 15px 32px;
      text-align: center;
      text-decoration: none;
      display: inline-block;
      font-size: 16px;
      margin-bottom: 3%;
      margin-top: 3%;
      margin-left: 5%;
    }
  }
}
</style>
