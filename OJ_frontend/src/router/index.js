import Vue from 'vue'
import Router from 'vue-router'
import Register from '@/components/Register'
import Login from "@/components/Login";
import Home from "@/components/Home"
import ProblemList from "@/components/Probelm/ProblemList";
import Status from "@/components/Probelm/Status";
import Problem from "@/components/Probelm/Problem";
import Submit from "@/components/Probelm/Submit";


import Code from "@/components/Code";

import ProblemEdit from "@/components/Probelm/ProblemEdit";





Vue.use(Router)


const routes = [
    {
        path: '/register',
        name: 'register',
        component: Register,
        meta: {
            title: '注册'
        }
    },
    {
        path: '/login',
        name: 'login',
        component: Login,
        meta: {
            title: '登录'
        }
    },
    {
        path: '/',
        name: 'home',
        component: Home,
        meta: {
            title: '主页',
            name: '首页'
        },

        children: [{
                path: '/problemList',
                component: ProblemList,
                meta: {
                    name: '题目'
                }
            }, {
                path: '/problemEdit',
                component: ProblemEdit,
                meta: {
                    name: '编辑题目'
                }
            }, {
                path: '/status',
                component: Status,
                meta: {
                    name: '提交状态'
                }
            }, {
                path: '/problem',
                component: Problem,
                meta: {
                    name: '题目描述'
                }
            }, {
                path: '/submit',
                component: Submit,
                meta: {
                    name: '题目提交'
                }
            },  {
                path: '/code',
                component: Code,
                meta: {
                    name: '完整代码'
                }
            }










        ]

    }

]

const router = new Router({
    mode: 'history',
    base: process.env.BASE_URL,
    routes
})

export default router
