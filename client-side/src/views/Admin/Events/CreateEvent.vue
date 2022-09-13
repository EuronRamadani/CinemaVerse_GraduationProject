<template>
      <div class="createPost">
      <form action="">
        <div class="container">
          <h2>Add an Event</h2>
          <input required type="text" placeholder="Enter event name" />
          <textarea />
          <p>Is Paid?</p>
          <label class="switch">
            <input type="checkbox" />
            <span class="slider"></span>
          </label>
          <div class="calendar">
          <p>Pick date</p>
            <v-date-picker v-model="picker"></v-date-picker>
          </div>
          <button type="submit">Submit</button>
        </div>
      </form>
    </div>
</template>

<script>
export default {
    name:"createEvent"
}
</script>


<style lang="scss" scoped>
@media (max-width: 900px) {
  .createPost {
    justify-content: center !important;
    margin-right: 0%;
    margin-left: 0%;
  }
}

.createPost {
  @media (max-width: 900px) {
    .container {
      width: 100% !important;
      flex-wrap: nowrap;
      justify-content: center;
    }
  }

  width: 100%;
  margin-top: 5%;
  display: flex;
  flex-direction: row;
  justify-content: left;
  padding: 15px;
  border: 3px solid rgb(25, 118, 210);

  .container {
    padding: 15px;
    margin-top: 3%;
    margin-bottom: 3%;
    width: 160%;
    display: flex;
    flex-direction: column;
    justify-content: center;

    .calendar{
      margin-top: 2%;
      margin-bottom: 2%;
    }

    input {
      border: 1px solid black;
      font-size: 20px;
      height: 20%;
      width: auto;
      padding: 15px;
      margin-bottom: 3%;
      margin-top: 3%;
    }

    textarea {
      font-size: 20px;
      height: 20%;
      width: auto;
      padding: 15px;
      -webkit-box-sizing: border-box;
      -moz-box-sizing: border-box;
      box-sizing: border-box;
      border: 3px solid black;
      margin-bottom: 3%;
      margin-top: 3%;
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
    }
  }

  .switch {
    --button-width: 3.5em;
    --button-height: 2em;
    --toggle-diameter: 1.5em;
    --button-toggle-offset: calc(
      (var(--button-height) - var(--toggle-diameter)) / 2
    );
    --toggle-shadow-offset: 10px;
    --toggle-wider: 3em;
    --color-grey: #cccccc;
    --color-green: #4296f4;
  }

  .slider {
    display: inline-block;
    width: var(--button-width);
    height: var(--button-height);
    background-color: var(--color-grey);
    border-radius: calc(var(--button-height) / 2);
    position: relative;
    transition: 0.3s all ease-in-out;
  }

  .slider::after {
    content: "";
    display: inline-block;
    width: var(--toggle-diameter);
    height: var(--toggle-diameter);
    background-color: #fff;
    border-radius: calc(var(--toggle-diameter) / 2);
    position: absolute;
    top: var(--button-toggle-offset);
    transform: translateX(var(--button-toggle-offset));
    box-shadow: var(--toggle-shadow-offset) 0
      calc(var(--toggle-shadow-offset) * 4) rgba(0, 0, 0, 0.1);
    transition: 0.3s all ease-in-out;
  }

  .switch input[type="checkbox"]:checked + .slider {
    background-color: var(--color-green);
  }

  .switch input[type="checkbox"]:checked + .slider::after {
    transform: translateX(
      calc(
        var(--button-width) - var(--toggle-diameter) -
          var(--button-toggle-offset)
      )
    );
    box-shadow: calc(var(--toggle-shadow-offset) * -1) 0
      calc(var(--toggle-shadow-offset) * 4) rgba(0, 0, 0, 0.1);
  }

  .switch input[type="checkbox"] {
    display: none;
  }

  .switch input[type="checkbox"]:active + .slider::after {
    width: var(--toggle-wider);
  }

  .switch input[type="checkbox"]:checked:active + .slider::after {
    transform: translateX(
      calc(
        var(--button-width) - var(--toggle-wider) - var(--button-toggle-offset)
      )
    );
  }
}
</style>
