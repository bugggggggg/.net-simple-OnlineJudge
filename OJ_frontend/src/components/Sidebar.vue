<template>

  <el-aside class="sidebar">

    <el-menu router :default-active="activeIndex" :collapse="this.$store.state.sideBarCollapse">
      <img src="../assets/tongji_logo.png" class="avatar"/>

        <!-- <el-image
            style="width: 50px; height: 50px"
            src="../assets/tongji_logo.png">
        </el-image> -->
      <el-menu-item index="/">
        <i class="el-icon-s-home"></i>
        <a slot="title">首页</a>
      </el-menu-item>



      <el-menu-item index="/problemList">
        <i class="el-icon-s-order"></i>
        <a slot="title">题目</a>
      </el-menu-item>
      <el-menu-item index="/status">
        <i class="el-icon-s-data"></i>
        <a slot="title">提交状态</a>
      </el-menu-item>

<!--
      <el-menu-item index="/news">
        <i class="el-icon-s-management"></i>
        <a slot="title">资讯</a>
      </el-menu-item>

      <el-menu-item index="/game">
        <i class="el-icon-s-shop"></i>
        <a slot="title">游戏</a>
      </el-menu-item> -->

    </el-menu>
  </el-aside>

</template>

<script>
export default {
  name: 'Sidebar',
  computed:{
    // 计算路由事件并高亮
    // activeIndex(){
    //     const route = this.$route
    //     const { meta, path } = route
    //     return meta.activeIndex
    // }
  },
  data () {
    return {
      // 初始化选中tab格
      activeIndex: "/"
    }
  },
  methods:{
  }
}
</script>

<!-- 导入sidebar css -->
<style scoped src="../assets/css/sidebar.css"></style>

<style>
    .el-submenu__title {
        color: rgb(191, 203, 217);
    }
</style>
