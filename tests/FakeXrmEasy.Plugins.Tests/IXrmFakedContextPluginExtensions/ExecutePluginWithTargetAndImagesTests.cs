﻿using FakeXrmEasy.Tests.PluginsForTesting;
using Microsoft.Xrm.Sdk;
using System;
using Xunit;

namespace FakeXrmEasy.Plugins.Tests.IXrmFakedContextPluginExtensions
{
    public class ExecutePluginWithTargetAndImagesTests
    {

        [Fact]
        public void When_passing_preentityimages_it_gets_added_to_context()
        {
            // arrange
            var context = new XrmFakedContext();
            var target = new Entity();

            EntityImageCollection preEntityImages = new EntityImageCollection();
            preEntityImages.Add("PreImage", new Entity());

            // act
            context.ExecutePluginWithTargetAndPreEntityImages<EntityImagesPlugin>(target, preEntityImages);

            // assert
            EntityImageCollection postImagesReturned = target["PostEntityImages"] as EntityImageCollection;

            if (postImagesReturned.Count > 0)
                throw new Exception("PostEntityImages should not be set.");

            EntityImageCollection preImagesReturned = target["PreEntityImages"] as EntityImageCollection;

            Assert.Equal(1, preImagesReturned?.Count);
            Assert.IsType(typeof(Entity), preImagesReturned["PreImage"]);
        }

        [Fact]
        public void When_passing_postentityimages_it_gets_added_to_context()
        {
            // arrange
            var context = new XrmFakedContext();
            var target = new Entity();

            EntityImageCollection postEntityImages = new EntityImageCollection();
            postEntityImages.Add("PostImage", new Entity());

            // act
            context.ExecutePluginWithTargetAndPostEntityImages<EntityImagesPlugin>(target, postEntityImages);

            // assert
            EntityImageCollection preImagesReturned = target["PreEntityImages"] as EntityImageCollection;

            if (preImagesReturned.Count > 0)
                throw new Exception("PreImages should not be set.");

            EntityImageCollection postImagesReturned = target["PostEntityImages"] as EntityImageCollection;

            Assert.Equal(1, postImagesReturned?.Count);
            Assert.IsType(typeof(Entity), postImagesReturned["PostImage"]);
        }
    }
}