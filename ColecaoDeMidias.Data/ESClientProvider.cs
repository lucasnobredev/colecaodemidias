﻿using ColecaoDeMidias.Domain;
using Nest;
using System;

namespace ColecaoDeMidias.Data
{
    public class ESClientProvider : IESClientProvider
    {
        public ESClientProvider()
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200/"))
                .DefaultIndex("midia");
            
            this.Client = new ElasticClient(settings);
            this.DefaultIndex = "midia";
            EnsureIndexWithMapping<IMidia>(this.DefaultIndex);
        }

        public ElasticClient Client { get; private set; }
        public string DefaultIndex { get; private set; }

        public void EnsureIndexWithMapping<T>(string indexName = null, Func<PutMappingDescriptor<T>, PutMappingDescriptor<T>> customMapping = null) where T : class
        {
            if (String.IsNullOrEmpty(indexName)) indexName = this.DefaultIndex;

            // Map type T to that index
            this.Client.ConnectionSettings.DefaultIndices.Add(typeof(T), indexName);

            // Does the index exists?
            var indexExistsResponse = this.Client.IndexExists(new IndexExistsRequest(indexName));
            if (!indexExistsResponse.IsValid) throw new InvalidOperationException(indexExistsResponse.DebugInformation);

            // If exists, return
            if (indexExistsResponse.Exists) return;

            // Otherwise create the index and the type mapping
            var createIndexRes = this.Client.CreateIndex(indexName);
            if (!createIndexRes.IsValid) throw new InvalidOperationException(createIndexRes.DebugInformation);

            var res = this.Client.Map<T>(m =>
            {
                m.AutoMap().Index(indexName);
                if (customMapping != null) m = customMapping(m);
                return m;
            });

            if (!res.IsValid) throw new InvalidOperationException(res.DebugInformation);
        }
    }

    public interface IESClientProvider
    {
        ElasticClient Client { get; }
        string DefaultIndex { get; }
    }
}
