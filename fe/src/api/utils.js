import axios from 'axios'
import { Notification } from 'element-ui'

const service = axios.create({
    baseURL: 'http://127.0.0.1:8700',
    withCredentials: true,
    headers: {
        'Content-Type': 'application/json;charset=UTF-8'
    }
})

// 请求拦截
service.interceptors.request.use(
    config => { return config },
    err => { return err }
)

// 响应拦截
service.interceptors.response.use(
    res => {
        if (res.data.status === 0) {
            Notification.success({
                title: res.data.msg,
                message: new Date(),
            })
        } else {
            Notification.info({
                title: res.data.msg,
                message: new Date(),
            })
        }
        return res.data
    },
    err => {
        Notification.error({
            title: '出错了',
            message: toString(res),
            duration: 0
        })
        return err
    }
)

export default service;