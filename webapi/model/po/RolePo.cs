namespace webapi.model.po;

public class RolePo : EntityPo
{
    public string name { get; set; }
    public List<UserPo> UserPos { get; set; }
}