﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_9H
{
    public enum CodeEnum
    {
        系统异常 = -1,
        成功 = 0,
        失败 = 1,

        账号不存在 = 400001,
        密码错误 = 400002,

        保存授权信息失败 = 401001,
    }
}
