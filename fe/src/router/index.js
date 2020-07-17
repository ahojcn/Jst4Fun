import Vue from 'vue'
import Router from 'vue-router'
import HelloWorld from '@/components/HelloWorld'

import FindPwd from '@/components/FindPwd'

import StuLogin from '@/components/StuLogin'
import StuRegister from '@/components/StuRegister'
import StuIndex from '@/components/StuIndex'

import AdminLogin from '@/components/AdminLogin'
import AdminIndex from '@/components/AdminIndex'

import Login from '@/components/Login'

Vue.use(Router)

export default new Router({
    routes: [{
            path: '/',
            name: 'HelloWorld',
            component: HelloWorld,
        }, {
            path: '/StuLogin',
            name: 'StuLogin',
            component: StuLogin,
        },
      {
        path: '/Login',
        name: 'Login',
        component: Login,
      },
        {
            path: '/StuRegister',
            name: 'StuRegister',
            component: StuRegister,
        },
        {
            path: '/StuIndex',
            name: 'StuIndex',
            component: StuIndex,
        },
        {
            path: '/FindPwd',
            name: 'FindPwd',
            component: FindPwd,
        },
        {
            path: '/AdminLogin',
            name: 'AdminLogin',
            component: AdminLogin,
        },
        {
            path: '/AdminIndex',
            name: 'AdminIndex',
            component: AdminIndex,
        },
    ]
})
