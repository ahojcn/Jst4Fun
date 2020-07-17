<template>
  <div>
    <h1>
      Login
    </h1>
  </div>
</template>

<script>
  export default {
    name: "Login",
    data() {
      return {
      }
    },
    methods: {
      onClickLogin() {
        location.href = this.url
      }
    },
    mounted() {
    },
  }
</script>

<style scoped>

</style>
