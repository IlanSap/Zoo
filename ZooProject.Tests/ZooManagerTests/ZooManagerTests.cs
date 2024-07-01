using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooProject.Data;
using Xunit;

namespace ZooProject.Tests.ZooMangaerTests;

public class ZooManagerTests
{
    private readonly ZooManager _zooManager;

    public ZooManagerTests()
    {
        _zooManager = new ZooManager();
    }

    [Fact]
    public void ZooManager_CreateZoo_ReturnZoo()
    {
        // Arrange
        var zooSize = 10;
        var intervalSeconds = 5;
        var tracker = new GPSTracker();

        // Act
        var zoo = _zooManager.CreateZoo(zooSize, intervalSeconds, tracker);

        // Assert
        Assert.NotNull(zoo);
        Assert.Equal(intervalSeconds, zoo.IntervalSeconds);
        // Check if Zoo.ZooArea is equal to zooSize * zooSize
        Assert.Equal(zooSize, zoo.ZooArea.ZooMap.Length);
    }
}