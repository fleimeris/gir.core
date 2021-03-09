﻿using System.Linq;
using System.Threading.Tasks;
using Repository;

namespace Generator.Services.Writer
{
    internal class WriterService
    {
        private readonly WriteTypesService _writeTypesService;
        private readonly WriteDllImportService _writeDllImportService;
        private readonly WriteSymbolsService _writeSymbolsService;
        private readonly WriteRecordsService _writeRecordsService;
        private readonly WriteStaticService _writeStaticService;

        public WriterService(WriteTypesService writeTypesService, WriteDllImportService writeDllImportService, WriteSymbolsService writeSymbolsService, WriteRecordsService writeRecordsService, WriteStaticService writeStaticService)
        {
            _writeTypesService = writeTypesService;
            _writeDllImportService = writeDllImportService;
            _writeSymbolsService = writeSymbolsService;
            _writeRecordsService = writeRecordsService;
            _writeStaticService = writeStaticService;
        }

        public void Write(LoadedProject loadedProject, string outputDir)
        {
            if (loadedProject.Namespace.SharedLibrary is null)
                Log.Debug($"Not generating DLL import helper for namespace {loadedProject.Namespace.Name}: It is missing a shared library info.");
            else
                _writeDllImportService.WriteDllImport(loadedProject, outputDir);

            _writeTypesService.Write(
                projectName: loadedProject.Name,
                outputDir: outputDir,
                templateName: "delegate.sbntxt",
                subfolder: "Delegates",
                objects: loadedProject.Namespace.Callbacks,
                @namespace: loadedProject.Namespace
            );

            _writeTypesService.Write(
                projectName: loadedProject.Name,
                outputDir: outputDir,
                templateName: "class.sbntxt",
                subfolder: "Classes",
                objects: loadedProject.Namespace.Classes.Where(x => !x.IsFundamental),
                @namespace: loadedProject.Namespace
            );
            
            _writeTypesService.Write(
                projectName: loadedProject.Name,
                outputDir: outputDir,
                templateName: "fundamental.class.sbntxt",
                subfolder: "Classes",
                objects: loadedProject.Namespace.Classes.Where(x => x.IsFundamental),
                @namespace: loadedProject.Namespace
            );

            _writeTypesService.Write(
                projectName: loadedProject.Name,
                outputDir: outputDir,
                templateName: "interface.sbntxt",
                subfolder: "Interfaces",
                objects: loadedProject.Namespace.Interfaces,
                @namespace: loadedProject.Namespace
            );

            _writeTypesService.Write(
                projectName: loadedProject.Name,
                outputDir: outputDir,
                templateName: "enum.sbntxt",
                subfolder: "Enums",
                objects: loadedProject.Namespace.Enumerations,
                @namespace: loadedProject.Namespace
            );

            _writeTypesService.Write(
                projectName: loadedProject.Name,
                outputDir: outputDir,
                templateName: "enum.sbntxt",
                subfolder: "Enums",
                objects: loadedProject.Namespace.Bitfields,
                @namespace: loadedProject.Namespace
            );

            _writeRecordsService.Write(
                projectName: loadedProject.Name,
                outputDir: outputDir,
                records: loadedProject.Namespace.Records,
                @namespace: loadedProject.Namespace
            );

            _writeSymbolsService.Write(
                projectName: loadedProject.Name,
                outputDir: outputDir,
                templateName: "constants.sbntxt",
                subfolder: "Classes",
                name: "Constants",
                symbols: loadedProject.Namespace.Constants,
                @namespace: loadedProject.Namespace
            );

            _writeSymbolsService.Write(
                projectName: loadedProject.Name,
                outputDir: outputDir,
                templateName: "functions.sbntxt",
                subfolder: "Classes",
                name: "Functions",
                symbols: loadedProject.Namespace.Functions,
                @namespace: loadedProject.Namespace
            );
            
            _writeStaticService.Write(
                projectName: loadedProject.Name,
                outputDir: outputDir,
                templateName: "extensions.sbntxt",
                subfolder: "Classes",
                name: "Extensions",
                @namespace: loadedProject.Namespace
            );
        }
    }
}
