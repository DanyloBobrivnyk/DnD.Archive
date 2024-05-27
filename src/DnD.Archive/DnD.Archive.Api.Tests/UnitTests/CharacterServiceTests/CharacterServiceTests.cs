using AutoMapper;
using DnD.Archive.Api.DTOs.Request;
using DnD.Archive.Api.DTOs.Response;
using DnD.Archive.Api.Helpers.Automapper;
using DnD.Archive.Api.Models;
using DnD.Archive.Api.Services.Implementation;
using FluentAssertions;

namespace DnD.Archive.Api.Tests.UnitTests.CharacterServiceTests
{
    public class CharacterServiceTests : IClassFixture<CharacterServiceTestDatabaseFixture>
    {
        private readonly CharacterService _characterService;
        private readonly IMapper _mapper;

        public CharacterServiceTests(CharacterServiceTestDatabaseFixture databaseFixture)
        {
            var mappingProfile = new DnDArchiveMappingProfile();
            var mapperConfig = new MapperConfiguration(cnf => cnf.AddProfile(mappingProfile));
            Mapper mapper = new Mapper(mapperConfig);

            _mapper = mapper;
            _characterService = new CharacterService(databaseFixture.Context, mapper);
        }

        [Fact]
        public async Task GetCharactersBySpecialization_WithCorrectSpecialization_Should_ReturnSuccessResult()
        {
            const string specialization = "Warrior";

            var result = await _characterService.GetCharactersBySpecializationAsync(specialization);

            result.Should().NotBeNull();
            result.Count().Should().Be(3);
            result.First().Name.Should().Be("Aragorn");
        }

        [Fact]
        public async Task GetCharactersBySpecialization_WithWrongSpecialization_Should_ReturnEmptyResult()
        {
            const string specialization = "Monk";

            var result = await _characterService.GetCharactersBySpecializationAsync(specialization);

            result.Should().NotBeNull();
            result.Count().Should().Be(0);
        }

        [Fact]
        public async Task AddCharacter_WithCorrectData_Should_ReturnSuccessResult()
        {
            var characterToAdd = new Character { Name = "DSV", Bio = "A stealthy rogue.", Hitpoints = 70, Manapool = 50, SpecializationName = "Rogue", Skills = new List<Skill> { } };
            var expected = _mapper.Map<CharacterDTOResponse>(characterToAdd);

            var characterRequestDto = new CharacterDTORequest { Name = characterToAdd.Name, Bio = characterToAdd.Bio, Hitpoints = characterToAdd.Hitpoints, Manapool = characterToAdd.Manapool, SpecializationName = characterToAdd.SpecializationName };
            
            var addedCharacterGuid = await _characterService.AddCharacterAsync(characterRequestDto);
            var actualResponse = await _characterService.GetCharacterByIdAsync(addedCharacterGuid);
            
            expected.Should().NotBeNull();
            expected.Should().BeEquivalentTo(actualResponse);
        }

    }
}