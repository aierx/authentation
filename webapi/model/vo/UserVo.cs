﻿namespace webapi.model.vo;

public class UserVo
{
    public long Id { set; get; }
    public string Name{ get; set; }
    
    public List<RoleVo> RoleVos { get; set; }

}