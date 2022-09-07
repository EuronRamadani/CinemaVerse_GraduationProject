<template>
  <div class="container">
    <div class="landing">
      <h3>View events in cinemas near you!</h3>
      <p>Events can be free or paid depending on the Cinema</p>
    </div>
    <div class="events">
      <div class="item" @click="divOnClick">
        <div class="ispaid">
          <p>This is a test post</p>
          <!-- show this conditionally if is paid or not TODO v-if="isPaid-->
          <p>$</p>
        </div>
        <img src="../../../src/assets/joker.jpg" alt="none" />
      </div>
      <div class="item" @click="divOnClick">
        <div class="ispaid">
          <p>This is a test post</p>
          <!-- show this conditionally if is paid or not TODO v-if="isPaid-->
          <p>$</p>
        </div>
        <img src="#" alt="none" />
      </div>
      <div class="item" @click="divOnClick">
        <div class="ispaid">
          <p>This is a test post</p>
          <!-- show this conditionally if is paid or not TODO v-if="isPaid-->
          <p>$</p>
        </div>
        <img src="#" alt="none" />
      </div>
      <div class="item" @click="divOnClick">
        <div class="ispaid">
          <p>This is a test post</p>
          <!-- show this conditionally if is paid or not TODO v-if="isPaid-->
          <p>$</p>
        </div>
        <img src="#" alt="none" />
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "events",

  methods: {
    divOnClick() {
      console.log("test");
      this.$router.push({ name: "Event" });
    },
  },
};
</script>

<style lang="scss" scoped>
.container {
  width: 80%;
  margin-right: 10%;
  margin-left: 10%;
  padding: 0;
  height: auto;
  .landing {
    padding: 20px;
    display: block;
    display: flex;
    flex-direction: column;

    h3 {
      text-align: center;
      font-size: 32px;
      font-family: "Rubik", sans-serif;
    }

    p {
      text-align: center;
      font-size: 20px;
      font-family: "Poppins", sans-serif;
    }
  }

  .events {
    display: flex;
    flex-direction: row;
    flex-wrap: wrap;
    margin-top: 3%;
    margin-bottom: 3%;
    margin-left: 0%;
    margin-right: 0%;
    width: 100%;
    height: auto;
    justify-content: space-between;

    @media (max-width: 900px) {
      flex-direction: column;
    }

    .item {
      border: 3px solid black;
      width: 45%;
      height: 500px;
      margin-bottom: 50px;
      padding: 10px;
      border-radius: 20px;

      @media (max-width: 900px) {
        width: 100%;
      }
      .ispaid {
        @media (max-width: 900px) {
          text-align: center;
          flex-direction: column;
        }

        p {
          font-size: 28px;
          padding: 10px;
          height: 20%;
          width: 50%;
        }

        display: flex;
        flex-direction: row;
        width: 100%;
      }

      img {
        height: 80%;
        width: 100%;
      }
      @media (max-width: 900px) {
        img{
          height: 60%;
        }

      .ispaid {
        width: 80%;
        margin-right: 10%;
        margin-left: 10%;

        p{
          text-align: center;
          margin-right: 25%;
          margin-left: 25%;
        }
      }

      }
    }
  }
}
</style>
