<template>
  <div class="container">
    <div class="header">
      <img :src="event.photos[0].imgClientPath" alt="no" />
      <div class="informationDiv">
        <h3>{{ event.name }}</h3>
        <p>
          This event is being hosted at {{ cinema.name }} in the city of
          {{ cinema.city }} with the address {{ cinema.address }}
        </p>
        <p v-if="event.isPaid">
          This event will cost {{ event.price }}€ to attend
        </p>
      </div>
    </div>
    <div class="content">
      <h4>{{ event.name }}</h4>
      <p>
        {{ event.description }}
      </p>
    </div>
    <div>
      <button-f buttonText="Go to event" />
    </div>
  </div>
</template>

<script>
import ButtonF from "../../components/uiComponents/ButtonF.vue";
import { required, numberInt, minValueRule } from "@/helpers/validations";
import { setInteractionMode } from "vee-validate";

setInteractionMode("eager");
export default {
  name: "Event",
  components: { ButtonF },

  data() {
    return {
      cinemaId: null,
      required,
      numberInt,
      minValueRule,
    };
  },
  created() {
    this.eventId = this.$route.params.eventId;
    this.cinemaId = this.cinema.id;
    this.getEvent(this.eventId);
  },
  filters: {
    truncate: function (text, length, suffix) {
      if (text.length > length) {
        return text.substring(0, length) + suffix;
      } else {
        return text;
      }
    },
  },
  computed: {
    loading() {
      return this.$store.state.events.loading;
    },
    event() {
      return this.$store.state.events.event;
    },
    cinema() {
      return this.$store.state.cinemas.cinema;
    },
	user(){
		return this.$store.state.users.user;
	}
  },
  methods: {
    submit() {
      this.$refs.observer.validate();
    },
    getEvent(eventId) {
      const query = {
        cinemaId: this.cinemaId,
        eventId: eventId,
      };
      this.$store
        .dispatch("getEvent", query)
        .then(() => {})
        .catch((error) => {
          this.errorToast(
            error.response?.data?.errors[0] ||
              "Something went wrong while fetching event details!"
          );
        });
    },
  },
};
</script>

<style lang="scss" scoped>
.container {
  margin-right: 10%;
  margin-left: 10%;
  width: 80%;
  padding: 0;
  height: auto;
  border-radius: 20px;
  border: 3px solid black;

  .header {
    border-radius: 20px !important;
    width: 100%;
    height: auto;
    display: flex;
    flex-direction: row;
    flex-wrap: nowrap;

    img {
      width: 50%;
      height: 400px;
      border-radius: 20px !important;
    }

    .informationDiv {
      width: 50%;
      height: 400px;
      padding: 16px;
      display: flex;
      flex-direction: column;

      div {
        width: 100%;
        height: 50%;
      }

      p {
        font-size: 22px;
        color: rgb(25, 118, 210);
      }
    }
  }

  @media (max-width: 900px) {
    .header {
      flex-direction: column;
      width: 100%;

      img {
        width: 100%;
      }

      .informationDiv {
        width: 100%;

        h3 {
          text-align: center;
        }

        p {
          text-align: center;
        }
      }
    }
  }

  .content {
    height: auto;
    width: 100%;
    padding: 15px;

    h4 {
      text-align: center;
      font-size: 32px;
      margin-bottom: 3%;
      margin-top: 3%;
      color: black;
    }

    p {
      font-size: 22px;
      padding: 10px;
      color: rgb(25, 118, 210);
    }
  }
}
</style>
