<template>

  <div>
    <el-card class="code_container">
      <pre>{{code}}</pre>
    </el-card>
  </div>
</template>

<script>
export default {
  name: "Code",
  data(){
    return{
      submissionId: 1,
      code: ''
    }

  },
  created() {
    this.submissionId = window.location.href.split("?")[1].split("=")[1];
    this.getCode();
  },
  methods: {
    getCode: function() {

      const url = this.APi;
      this.$axios.get(url + 'api/submission/getSubmissionById',
          { params: { submissionId:this.submissionId }})
          .then((response) => {
            //console.log(response);
            console.log("代码获取成功")
            const data = response.data[0];

              this.code = data.submissionCode;
          })
    }
  }
}
</script>

<style scoped>
.code_container {
  margin-left: 10px;
  margin-right: 10px;
  margin-top: 10px;
}
</style>
