const helpers = {
  methods: {
    getOptions: (options) =>
      options.map((o) => ({ text: o.value, value: o.id })),
    getObjectOptions: (options) => {
      const keys = Object.keys(options);
      return keys.map((key) => ({ text: key, value: options[key] }));
    },
    getObjectOptionsName: (options) => {
      const keys = Object.keys(options);
      return keys.map((key) => ({
        text: options[key].name,
        value: options[key],
      }));
    },
    getObjectText: (options, id) => options.find((o) => o.value === id)?.text,
    getObjectName: (options, id) => options.find((o) => o.value === id)?.name,
    getObjectOptionsText: (options, id) => {
      const keys = Object.keys(options);
      const opt = keys.map((key) => ({ text: key, value: options[key] }));
      return opt.find((o) => o.value === id)?.text;
    },
  },
};

export default helpers;
