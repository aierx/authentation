namespace webapi.model.po;

public class ResourcePo : EntityPo
{
    public string name { get; set; }

    /**
     * 1 url
     * 2 appkey
     */
    public int type { get; set; }

    public List<PermissionPo> PermissionPos { get; set; }

    public List<RolePo> RolePos { get; set; }
}