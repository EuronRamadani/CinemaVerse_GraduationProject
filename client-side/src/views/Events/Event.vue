<template>
  <div class="container">
    <div class="header">
      <img src="#" alt="no" />
      <div class="informationDiv">
        <h3>This is a test post title</h3>
        <p>
          Sed quis libero eget est tincidunt gravida et sed orci. In pulvinar
          rhoncus nulla a ullamcorper. Vestibulum et mi nec justo pretium
          tristique eget nec tellus.
        </p>
      </div>
    </div>
    <div class="content">
      <h4>Post title a second time</h4>
      <p>
        Dedicated post description Pellentesque elementum leo quis iaculis
        sodales. Maecenas et purus ligula. Vestibulum ex dolor, pellentesque id
        viverra vel, venenatis in orci. Cras feugiat erat odio, sed auctor enim
        suscipit ut. Lorem ipsum dolor sit amet, consectetur adipiscing elit.
        Aenean vitae tellus pretium, pretium dolor nec, dignissim dolor.
        Pellentesque quis lectus elit. Pellentesque habitant morbi tristique
        senectus et netus et malesuada fames ac turpis egestas.
      </p>
    </div>
    <div>
      <button-f buttonText="Go to event" />
    </div>
  </div>
</template>

<script>
import ButtonF from "../../components/uiComponents/ButtonF.vue";
export default {
  name: "Event",
  components: { ButtonF },
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
