namespace webapi.model.po;

public class UserPo : EntityPo
{
    public string name { get; set; }
    
    public string passwd { set; get; }
    public List<RolePo> RolePos { get; set; }
}