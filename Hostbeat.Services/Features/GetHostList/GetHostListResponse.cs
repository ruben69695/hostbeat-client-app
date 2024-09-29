using System.Collections.Generic;

namespace Hostbeat.Services.Features.GetHostList;

public class GetHostListResponse
{
    public List<HostItemResponse> Data { get; set; }

    private GetHostListResponse(List<HostItemResponse> list)
    {
        Data = list;
    }

    public static GetHostListResponse CreateWithList(List<HostItemResponse> list)
    {
        return new GetHostListResponse(list);
    }
}