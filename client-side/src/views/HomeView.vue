<template>
  <div>
    <!-- <h1>Homepage</h1> -->
    <UpcomingMovies />
    <section id="features" class="grey lighten-3">
      <div class="py-12"></div>
      <v-container class="text-center">
        <h2 class="display-2 font-weight-bold mb-3">CINEMAVERSE FEATURES</h2>

        <v-responsive class="mx-auto mb-12" width="56">
          <v-divider class="mb-1"></v-divider>

          <v-divider></v-divider>
        </v-responsive>

        <v-row>
          <v-col
            v-for="({ icon, title, text }, i) in features"
            :key="i"
            cols="12"
            md="4"
          >
            <v-card class="py-12 px-4" color="grey lighten-5" flat>
              <v-theme-provider dark>
                <div>
                  <v-avatar color="primary" size="88">
                    <v-icon large v-text="icon"></v-icon>
                  </v-avatar>
                </div>
              </v-theme-provider>

              <v-card-title
                class="justify-center font-weight-black text-uppercase"
                v-text="title"
              ></v-card-title>

              <v-card-text class="subtitle-1" v-text="text"> </v-card-text>
            </v-card>
          </v-col>
        </v-row>
      </v-container>

      <div class="py-12"></div>
    </section>
    <section id="stats">
      <v-parallax
        :height="$vuetify.breakpoint.smAndDown ? 700 : 500"
        src="https://images.unsplash.com/photo-1489599849927-2ee91cede3ba?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80"
      >
        <v-container fill-height>
          <v-row class="mx-auto">
            <v-col
              v-for="[value, title] of stats"
              :key="title"
              cols="12"
              md="3"
            >
              <div class="text-center">
                <div
                  class="display-3 font-weight-black mb-4"
                  v-text="value"
                ></div>

                <div
                  class="title font-weight-regular text-uppercase"
                  v-text="title"
                ></div>
              </div>
            </v-col>
          </v-row>
        </v-container>
      </v-parallax>
    </section>
    <div>
      <v-main class="grey lighten-2">
        <v-container>
          <v-row>
            <p>Qitu mi shfaq do movies</p>
            <v-col v-for="j in 6" :key="`${n}${j}`" cols="6" md="2">
              <v-sheet height="150">
                <!-- <img :src="cinemas" alt="" /> -->
              </v-sheet>
            </v-col>
          </v-row>
        </v-container>
      </v-main>
    </div>

    <div>
      <v-main class="grey lighten-2">
        <v-container>
          <v-row>
            <p>evente</p>
            <v-col v-for="n in 6" :key="n" cols="6">
              <v-card height="200"></v-card>
            </v-col>
          </v-row>
        </v-container>
      </v-main>
    </div>
    <div class="container">
      <section class="about-container container">
        <div class="about-sections">
          <img class="offer-image" :src="photo1" alt="" />
          <div class="description">
            <h3>About Us</h3>
            <p>
              CinemaVerse, as the first project in our region for the union of
              cinemas, is a big project that is doing very well because it is
              easy to use and you have everything in one place. CinemaVerse was
              created with the aim of bringing customers together in one
              platform, through comments, movie selection and online payment.
            </p>
          </div>
        </div>
        <div class="about-sections">
          <div class="description">
            <h3>Our Mission</h3>
            <p>
              As an application that is at the beginning, we are trying to be
              the best in the work we do, where we still continue to work
              towards the quality and maintenance of the application. Our
              mission for the future is to spread even more through the
              neighboring countries, to have as many cinemas from different
              countries as possible and in this way to expand even more.
            </p>
          </div>
          <img class="offer-image" :src="photo2" alt="" />
        </div>
        <div class="about-sections">
          <img class="offer-image" :src="photo3" alt="" />
          <div class="description">
            <h3>Experience</h3>
            <p>
              Considering that CinemaVerse is the first application in our
              market, it has been well received by our clients, where we have
              also received feedback, very good comments. So we are working for
              the best
            </p>
          </div>
        </div>
      </section>
    </div>
  </div>
</template>

<script>
import UpcomingMovies from "../components/SlideshowMovies.vue";
// import { mapActions, mapGetters } from "vuex";

export default {
  name: "Home",
  components: {
    UpcomingMovies,
  },
  data() {
    return {
      stats: [
        ["1500+", "Tickets Sold"],
        ["145+", "Movies Streamed"],
        ["2300+", "Visits"],
        ["500+", "Events"],
      ],
      features: [
        {
          icon: "mdi-account-group-outline",
          title: "Vibrant Community",
          text: "Lorem ipsum dolor sit amet consectetur adipisicing elit. Iusto cupiditate sint possimus quidem atque harum excepturi nemo velit tempora! Enim inventore fuga, qui ipsum eveniet facilis obcaecati corrupti asperiores nam",
        },
        {
          icon: "mdi-update",
          title: "Frequent Updates",
          text: "Sed ut elementum justo. Suspendisse non justo enim. Vestibulum cursus mauris dui, a luctus ex blandit. Lorem ipsum dolor sit amet consectetur adipisicing elit. qui ipsum eveniet facilis obcaecati corrupti consectetur adipisicing elit.",
        },
        {
          icon: "mdi-shield-outline",
          title: "Long-term Support",
          text: "Lorem ipsum dolor sit amet consectetur adipisicing elit. Iusto cupiditate sint possimus quidem atque harum excepturi nemo velit tempora! Enim inventore fuga, qui ipsum eveniet facilis obcaecati corrupti asperiores nam",
        },
      ],
      drawer: null,
      photo1:
        "https://www.cineplexx-ks.eu/GenticsImageStore/952/353/force//media/mobile/ks/center/5.jpg",
      photo2:
        "https://qkk-rks.com/uploads/images/2018/March/12/auto_cineplexx1520872988.jpg",
      photo3:
        "https://qkk-rks.com/uploads/images/2018/March/12/auto_kinoabc11520873925.jpg",
    };
  },
  created() {
    this.getCinemas();
    console.log("Created", this.cinemas);
  },
  computed: {
    cinemas() {
      return this.$store.state.cinemas.cinemas;
      // [0].photos[0].imgClientPath
    },
  },
  methods: {
    getCinemas() {
      this.$store.dispatch("getCinemas").catch((error) => {
        this.errorToast(
          error.response?.data?.errors[0] ||
            "Something went wrong while fetching cinemas!"
        );
      });
    },
  },
};
</script>

<style scoped>
.container {
  width: 95%;
  height: 70%;
  margin: auto;
  overflow: hidden;
}
.offer-image {
  height: 300px;
}
.description {
  padding: 50px;
}
.about-container {
  display: flex;
  flex-direction: column;
}
.about-sections {
  display: flex;
  align-items: center;
  background-color: #fff;
  margin: 50px 0 50px 0;
}
</style>
