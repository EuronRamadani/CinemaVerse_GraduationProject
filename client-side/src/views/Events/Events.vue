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
  margin: 0;
  padding: 0;
  height: auto;
  background-color: rgb(21, 26, 33);
  .landing {
    background-color: rgb(21, 26, 33);
    padding: 20px;
    display: block;
    display: flex;
    flex-direction: column;

    h3 {
      text-align: center;
      font-size: 32px;
      font-family: "Rubik", sans-serif;
      color:rgb(221, 118, 43);
    }

    p {
      text-align: center;
      font-size: 20px;
      font-family: "Poppins", sans-serif;
      color:white;
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

    @media (max-width: 900px) {
      flex-direction: column;
    }

    .item {
      border: 4px solid rgb(221, 118, 43);
      width: 50%;
      height: 500px;

      @media (max-width: 900px) {
        width: 100%;
      }
      .ispaid {
        @media (max-width: 900px) {
          text-align: center;
          flex-direction: column;
        }
        p {
          font-size: 32px;
          padding: 15px;
          color: white;
          height: 20%;
          width: 100%;
        }

        display: flex;
        flex-direction: row;
        justify-content: space-between;
      }

      img {
        height: 80%;
        width: 100%;
      }
    }
  }
}
</style>