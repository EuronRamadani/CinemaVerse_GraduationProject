<template>
  <div class="container">
    <div class="header">
      <img src="#" alt="no" />
      <div class="info">
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
  margin: 0;
  padding: 0;
  height: auto;
  background-color: rgb(21, 26, 33);

  .header {
    width: 100%;
    height: auto;
    display: flex;
    flex-direction: row;
    flex-wrap: nowrap;

    img {
      width: 50%;
      height: 400px;
    }

    .info {
      width: 50%;
      height: 400px;
      padding: 16px;
      display: flex;
      flex-direction: column;
      background-color: rgb(221, 118, 43);

      div {
        width: 100%;
        height: 50%;
      }

      p {
        font-size: 22px;
        color: white;
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
      color: rgb(221, 118, 43);
    }

    p {
      font-size: 22px;
      padding: 10px;
      color: white;
    }
  }
}
</style>
